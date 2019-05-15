import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import LinkedList from './components/LinkedList';

var arr1 = [{ name: 'caca', value: 123 }, { name: 'maca', value: 124 }, { name: 'eee', value: 125 }, { name: 'dddd', value: 126 }];
var arr2 = [{ name: 'caca2', value: 1 }, { name: 'maca2', value: 2 }, { name: 'eee2', value: 3 }, { name: 'dddd2', value: 4 }];

class App extends Component {
    render() {
        return (
            <div className="app">
                <LinkedList list1={arr1} list2={arr2} />
            </div>
        );
    }
}

export default App;
