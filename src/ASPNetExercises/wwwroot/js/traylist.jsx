//
// ReactBootstrap Component variables
//
var ListGroup = ReactBootstrap.ListGroup;
var ListGroupItem = ReactBootstrap.ListGroupItem;
var Modal = ReactBootstrap.Modal;

// example of how to do inline style
var qtyStyle = {
    textAlign: "left",
    position: "relative",
    width: "10%",
    paddingRight: "10px"
}
//
// ModalDetails Component
//
var ModalDetails = React.createClass({
    render: function () {
        return (
        <ListGroupItem>
        <span style={qtyStyle}>{this.props.details.Qty}</span>
    <span>{this.props.details.Description}</span>
    </ListGroupItem>
)
}
});
//
// Tray Component
//
var Tray = React.createClass({
    getInitialState() {
        return { showModal: false, traydetails: [] };
    },
    close() {
        this.setState({ showModal: false });
    },
    open() {
        this.setState({ showModal: true });
        var tray = this.props.tray;
        var url = this.props.source + "/" + tray.Id;
        httpGet(url, function (data) {
            this.setState({ traydetails: data });
        }.bind(this));
    },
    render: function () {
        var detailsForModal = this.state.traydetails.map(details =>
            <ModalDetails details={details} key={details.TrayId} />
        );
        return (
         <div>
            <ListGroupItem onClick={this.open}>
              <span className="col-xs-3 text-left">{this.props.tray.Id}</span>
              <span className="col-xs-9 orderline">{formatDate(this.props.tray.DateCreated)}</span>
            </ListGroupItem>

            <Modal show={this.state.showModal} onHide={this.close}>
                <Modal.Header closeButton>
                    <Modal.Title>
                        <div>
                            <span className="col-xs-12">&nbsp;</span>
                            <span className="col-xs-3 text-center">Tray:{this.props.tray.Id}</span>
                            <span className="col-xs-9 text-right xsmallFont">Date:{formatDate(this.props.tray.DateCreated)}</span>
                        </div>
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <ListGroup>
                    <div className="text-center navbar navbar-default top25" style={{marginBottom:"0px"}}>
                        <div className="col sm-4 col-xs-1 top10 bold">Qty</div>
                        <div className="col-sm-8 col-xs-10 top10 bold">Description</div>
                    </div>{detailsForModal}
                    </ListGroup>
                </Modal.Body>
                <Modal.Footer>
                    <div className="text-right">
                        <span className="col-xs-10">Total Calories:</span>
                        <span className="col-xs-2">{this.props.tray.TotalCalories}</span>
                        <span className="col-xs-10">Total Protein (g):</span>
                        <span className="col-xs-2">{this.props.tray.TotalProtein}</span>
                        <span className="col-xs-10">Total Fat (g):</span>
                        <span className="col-xs-2">{this.props.tray.TotalFat}</span>
                        <span className="col-xs-10">Total Salt (mg):</span>
                        <span className="col-xs-2">{this.props.tray.TotalSalt}</span>
                        <span className="col-xs-10">Total Cholesterol:</span>
                        <span className="col-xs-2">{this.props.tray.TotalCholesterol}</span>
                    </div>
                </Modal.Footer>
            </Modal>
          </div>
        )
    }
});
//
// TrayList Component
//
var Traylist = React.createClass({
    getInitialState: function () {
        return ({ trays: [] });
    },
    componentDidMount: function () {
        httpGet(this.props.source, function (data) {
            this.setState({ trays: data });
        }.bind(this));
    },
    render: function () {
        var trays = this.state.trays.map(tray => <Tray tray={tray} key={tray.Id} source="/GetTrayDetailsAsync" />);
        return (
            <div className="top25">
                <div className="col-sm-4 col-xs-1">&nbsp;</div>
                <div className="col-sm-4 col-xs-12">
                    <div className="panel-title text-center">
                        <h3>Trays You've Saved</h3>
                        <img className="smaller-img" src="/img/Tray.png" />
                    </div>
                    <div className="panel-body">
                    <div>
                        <div className="text-center navbar navbar-default" style={{top:"25px",
                            position:"relative"}}>
                            <div className="col sm-4 col-xs-2" style={{top:"10px",
                            position:"relative"}}>#</div>
                            <div className="col-sm-8 col-xs-10" style={{top:"10px",
                            position:"relative"}}>Date</div>
                        </div>
                        <ListGroup>
                            {trays}
                        </ListGroup>
                    </div>
                    </div>
                </div>
            </div>
        )
    }
})

ReactDOM.render(
<Traylist source="/GetTrays" />,
document.getElementById("traylist") // html tag
)
