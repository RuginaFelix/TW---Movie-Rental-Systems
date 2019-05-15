import React from 'react'
import AssignmentTagComponent from "./AssignmentTagComponent";

class LinkedListComponent extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            selectedItemsFromList1: [],
            selectedItemsFromList2: [],
            List1: [],
            List2: []
        };
    }

    MoveEntry = (moveIndex, event) => {
        debugger;
        var itemsToMove = [];
        if (moveIndex == 2) {
            itemsToMove = this.state.selectedItemsFromList1;
        }
        else {
            itemsToMove = this.state.selectedItemsFromList2;
        }

        if (itemsToMove.length == 0) {
            alert("Please select at least 1 item to move");
            return;
        }

        var arr1 = this.state.List1;
        var arr2 = this.state.List2;
        var arrT = [];
        if (moveIndex == 2) {
            arrT = arr1;
        }
        else {
            arrT = arr2;
        }

        arrT.map(element => {
            itemsToMove.map(elementToMove => {
                if (element.value == elementToMove.id) {
                    elementToMove.classList.remove('selected-entry');
                    if (moveIndex == 2) {
                        arr1 = arr1.filter(function (elementT) { return elementT.value != element.value });
                        arr2.push(element);
                        this.setState({ List1: arr1, List2: arr2 });
                    } else {
                        arr2 = arr2.filter(function (elementT) { return elementT.value != element.value });;
                        arr1.push(element);
                        this.setState({ List1: arr1, List2: arr2 });
                    }
                }
            });
        });

        this.setState({ selectedItemsFromList1: [], selectedItemsFromList2: [] });
    }

    OnSelectEntry = (moveIndex, event) => {
        debugger;
        var element = event.target;
        var arr = [];

        if (moveIndex == 1) {
            arr = this.state.selectedItemsFromList1;
        }
        else {
            arr = this.state.selectedItemsFromList2;
        }

        if (arr.length == 0) {
            event.target.classList.add('selected-entry');
            arr.push(element);
            return;
        }

        var wasFound = false;

        for (var i = 0; i < arr.length; i++) {
            debugger;
            if (arr[i].id == element.id) {
                event.target.classList.remove('selected-entry');
                arr.pop(element);
                wasFound = true;
                break;
            }
        }
        if (!wasFound) {
            element.classList.add('selected-entry');
            arr.push(element);
        }

        if (moveIndex == 1) {
            this.setState({
                selectedItemsFromList1: arr,
                selectedItemsFromList2: []
            });
        }
        else {
            this.setState({
                selectedItemsFromList2: arr,
                selectedItemsFromList1: []
            });
        }
    }

    componentDidMount() {
        this.setState({ List1: this.props.list1, List2: this.props.list2 });
    }

    render() {

        const list1 = this.state.List1;
        const list2 = this.state.List2;
        const tableEntriesForTable1 = list1.map((element, index) => {
            return (
                <AssignmentTagComponent value={element.value} onClick={this.OnSelectEntry.bind(this, 1)} key={index} name={element.name} />
            );
        });
        const tableEntriesForTable2 = list2.map((element, index) => {
            return (
                <AssignmentTagComponent onClick={this.OnSelectEntry.bind(this, 2)} value={element.value} key={index} name={element.name} />
            );
        });
        return (
            <div>
                <div className="table-1">
                    {
                        tableEntriesForTable1
                    }
                </div>
                <div className="buttons-container">
                    <input type="button" onClick={this.MoveEntry.bind(this, 1)} value="move left" />
                    <input type="button" onClick={this.MoveEntry.bind(this, 2)} value="move right" />
                </div>
                <div className="table-2">
                    {
                        tableEntriesForTable2
                    }
                </div>
            </div>
        );
    }
}

export default LinkedListComponent;