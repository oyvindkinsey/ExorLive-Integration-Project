
Ext.BLANK_IMAGE_URL = '../resources/libs/ext-3.0.0/resources/images/default/s.gif';

function Application(){
    var EXORLIVEAUTHURL = SETTINGS.ExorLiveAuthUrl; 
    var EXORLIVEURL = SETTINGS.ExorLiveUrl;
    var APPLICATIONURL = location.href.substring(0, location.href.lastIndexOf("/"));
    var _oauthTokenWindow;
    var _selectedContactId;
    
	
    function _displayMessage(msg){
        if (Ext.MessageBox.isVisible()) {
            Ext.MessageBox.updateText(msg);
        }
        else {
            Ext.MessageBox.wait(msg);
        }
    }
	
    function _initExorLive(authKey){
        _displayMessage("Initializing ExorLive..");
        ExorLive.init({
            local: APPLICATIONURL + "/exorlive/hash.html",
            authURL: EXORLIVEAUTHURL + "/?authKey=" + authKey,
            culture: "nb-NO", //OPTIONAL
            orderURL: "https://order.int.exorlive.com/?culture=nb-NO&mode=campaign&code=xyz" //OPTIONAL
        });
    }
    
    function _retrieveOAuthToken(){
        _displayMessage("Retrieving OAuth token..");
        ExorLiveConsumer.Service.GetIdentityUrl(function(response){
            // Sign the user into ExorLive and redirect them back to request the OAuthToken
            var getOAuthTokenUrl = EXORLIVEAUTHURL + "/consumers/openid/?identifier=" +
            encodeURIComponent(response) +
            "&redirect=" +
            encodeURIComponent(APPLICATIONURL + "/exorlive/GetOAuthToken.ashx");
            _oauthTokenWindow = window.open(getOAuthTokenUrl, "oauthTokenWindow");
            if (!_oauthTokenWindow){
                _displayMessage("We were unable to open a necessary popup window - please allow this for this domain, and for the ExorLive domain");
            }
            
        });
    }
    
	
    function _createExorLiveAccount(){
        _displayMessage("Creating ExorLive account..");
        ExorLiveConsumer.Service.CreateExorLiveAccount(function(response){
            if (response.Status == Entities.CreateExorLiveAccountResponse.StatusType.Ok) {
                _openExorLive();
            }
            else {
                Ext.MessageBox.alert("Error creating account", response.ErrorMessage);
            }
        });
    }
	
    function _openExorLive(){
        if (!ExorLive.isInitialized()) {
            _displayMessage("Retrieving ExorLive authentication key..");
            ExorLiveConsumer.Service.GetExorLiveAuthKey(function(response){
                switch (response.Status) {
                    case Entities.ExorLiveAuthKeyResponse.StatusType.Ok:
                        // We have a valid authention key, lets initialize ExorLive
                        _initExorLive(response.AuthKey);
                        break;
                    case Entities.ExorLiveAuthKeyResponse.StatusType.NoExorLiveAccount:
                        // There is no ExorLive account attached to the user
                        Ext.MessageBox.show({
                            title: "No ExorLive account attached",
                            msg: "Your user is not set up with ExorLive, do you have a valid account or would you like us to create a demo account for you?",
                            buttons: {
                                yes: "I have a valid account",
                                no: "Please create one for me",
                                cancel: "Cancel"
                            },
                            fn: function(btn){
                                switch (btn) {
                                    case "yes":
                                        // The user has an account, lets get an OAuthToken
                                        _retrieveOAuthToken();
                                        break;
                                    case "no":
                                        // The user has no account, lets set one up
                                        _createExorLiveAccount();
                                        break;
                                }
                            }
                        });
                        break;
                    case Entities.ExorLiveAuthKeyResponse.StatusType.NoOAuthToken:
                        // We have an account, but no token, lets get one
                        _retrieveOAuthToken();
                        break;
                    case Entities.ExorLiveAuthKeyResponse.StatusType.InvalidOAuthToken:
                        // We have a token, but it is invalid, should we request a new one?
                        Ext.MessageBox.show({
                            title: "Invalid OAuthToken",
                            msg: "Our OAuthToken was invalid, this might be because you invalidated it in ExorLive." +
                            "<br/>Do you want us to request a new one?",
                            buttons: Ext.MessageBox.YESNO,
                            fn: function(btn){
                                if (btn == "yes") {
                                    _retrieveOAuthToken();
                                }
                            }
                        });
                        break;
                }
            });
        }
    }
	
    function _openContact(localId){
        if (ExorLive.isInitialized()) {
            _setSelectedExorLiveId(localId);
        }
        else {
            _selectedContactId = localId;
            _openExorLive();
        }
    }
    
    var _exorLiveIdHashTable = {};
    function _setSelectedExorLiveId(id){
        if (_exorLiveIdHashTable[id]) {
            ExorLive.setSelectedUserId(_exorLiveIdHashTable[id]);
        }
        else {
            _displayMessage("Retrieving ExorLive Id..");
            ExorLiveConsumer.Service.GetExorLiveId(id, function(response){
                Ext.MessageBox.hide();
                if (response.Status == Entities.GetExorLiveIdResponse.StatusType.Ok) {
                    _exorLiveIdHashTable[id] = response.ExorLiveId;
                    ExorLive.setSelectedUserId(response.ExorLiveId);
                }
                else {
                    alert(response.ErrorMessage);
                }
            });
        }
    }
    function _listClients(){
        ExorLiveConsumer.Service.ListClients(function(response){
            _clientStore.loadData(response);
        })
    }
    
    /* */
    var _fields = [{
        name: 'Id',
        type: "float"
    }, {
        name: 'FullName',
        type: 'string'
    }, {
        name: 'EMailAddress',
        type: 'string'
    }, {
        name: 'ExorLiveId',
        type: 'float'
    }];
    
    var _clientStore = new Ext.data.Store({
        fields: _fields,
        id: 0,
        reader: new Ext.data.JsonReader({
            id: "Id"
        }, Ext.data.Record.create(_fields))
    });
    /* setup ui */
    var _clientAction = new Ext.ux.grid.RowActions({
        keepSelection: true,
        autoWidth: true,
        actions: [{
            iconCls: 'icon-exorlive',
            tooltip: 'Open client in ExorLive',
            callback: function(grid, record, action, row, col){
                _openContact(record.data.Id);
            }
        }]
    });
  
    _viewport = new Ext.Viewport({
        layout: 'border',
        items: [{
            region: 'north',
            html: '<h1 class="x-panel-header">ExorLive Consumer - Sample CRM</h1>',
            autoHeight: true,
            border: false
        }, {
            region: 'center',
            tbar: [{
                text: "Open ExorLive",
                handler: function(){
                    _openExorLive();
                }
            }, {
                text: "Sign out",
                handler: function(){
                    location.href = "../logout.aspx";
                }
            }],
            items: {
                xtype: "tabpanel",
                activeItem: 0,
                items: [{
                    title: 'My Clients',
                    xtype: "grid",
                    height: 200,
                    store: _clientStore,
                    plugins: [_clientAction],
                    columns: [{
                        header: "Id",
                        sortable: true,
                        dataIndex: 'Id'
                    }, {
                        id: 'FullName',
                        header: "Fullname",
                        sortable: true,
                        dataIndex: 'FullName'
                    }, {
                        header: "EMail address",
                        sortable: true,
                        dataIndex: 'EMailAddress',
                        width: 200
                    }, _clientAction],
                    stripeRows: true,
                    listeners: {
                        afterRender: function(){
                            _listClients();
                        }
                    }
                }]
            }
        }]
    });
    
    ExorLive.on("windowloaded", function(){
        if (_selectedContactId) {
            _displayMessage("Selecting client ..");
            _setSelectedExorLiveId(_selectedContactId);
        }
        else {
            Ext.MessageBox.hide();
        }
    });
    
    /* */
    return {
        NotifyGotOAuthToken: function(){
            _oauthTokenWindow.close();
            _oauthTokenWindow = null;
            _openExorLive();
        }
    };
}

Ext.onReady(function(){
    Ext.QuickTips.init();
    window.app = Application();
});
