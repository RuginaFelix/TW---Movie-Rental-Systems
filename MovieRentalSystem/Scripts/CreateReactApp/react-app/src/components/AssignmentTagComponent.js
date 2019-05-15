import React, { Component } from 'react';

class AssignmentTagComponent extends Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div id={this.props.value} className={this.props.className} onClick={this.props.onClick}>{this.props.name}</div>
        );
    }
}

export default AssignmentTagComponent;