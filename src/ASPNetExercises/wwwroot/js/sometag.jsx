var SomeTag = React.createClass({
    getInitialState: function() {
        return {statevalue: this.props.someproperty + " World"};
    },
    render: function() {
        return (<h1>{this.state.statevalue}</h1>);
    }
});
ReactDOM.render(
<SomeTag someproperty="Hello"/>,
document.getElementById("example")
);