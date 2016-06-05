//
// ReactBootstrap Component variables
//
var ListGroup = ReactBootstrap.ListGroup;
var ListGroupItem = ReactBootstrap.ListGroupItem;
// example of how to do inline style with a var
var idStyle = {
    textAlign: "right",
    position: "relative",
    paddingLeft: "5%",
    paddingRight: "20%"
}
//
// ListItem Component
//
var ListItem = React.createClass({
    getInitialState() {
        return { itemdetails: [] };
    },
    render: function () {
        return (
            <ListGroupItem>
                <span style={idStyle} className="col-xs-2">{this.props.item.id}</span>
                <span style={idStyle} className="col-xs-5">{this.props.item.data}</span>
                <span >{this.props.item.data2}</span>
            </ListGroupItem>
        )
    }
});
//
// ListItems Component
//
var ListItems = React.createClass({
    getInitialState: function () {
        return ({
            listitems: [
                { "id": 1, "data": "data for 1", "data2": "more data for 1"},
                { "id": 2, "data": "data for 2", "data2": "more data for 2"},
                { "id": 3, "data": "data for 3", "data2": "more data for 3"}
            ]});
    },
    render: function () {
        var items = this.state.listitems.map(item =>
            <ListItem item={item}/>
        );
        return (
            <div style={{fontSize:"xx-large"}}>
            <div className="col-xs-2">&nbsp;</div>
            <div className="panel-body col-xs-8">
                <div>
                    <div className="text-center navbar navbar-default" style={{top:"25px", position:"relative"}}>
                        <div className="col-xs-2 top10">Key</div>
                        <div className="col-xs-5 top10">Data</div>
                        <div className="col-xs-5 top10">xData</div>
                    </div>
                    <ListGroup>
                    {items}
                    </ListGroup>
                </div>
            </div>
            </div>
        )
    }
})
ReactDOM.render(
    <ListItems />,
    document.getElementById("example") // html tag
)