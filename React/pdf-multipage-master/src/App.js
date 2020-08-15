import React, { Component } from 'react';
import './App.css';
import { onClick } from './logic';


class App extends Component {


  generateContent = () => {
    var str = '';
    for (var i = 0; i < 10000; i++) {
      str += `${i} ` 
    }
    return str;
  }

  render() {
    return (<div className="App">
      <div id="myMm" style={{ height: "1mm" }} />

      <button onClick={onClick}> To PDF! </button>
      <div id="html-2-pdfwrapper">
        {this.generateContent()}
      </div>
    </div>)
  }

}

export default App;
