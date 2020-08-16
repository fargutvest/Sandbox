import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';

class App extends Component {
  render() {
    return (
      <div className="App">
        <div className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h1>Hello React!</h1>
        </div>
        <div className="App-intro">
         <p>
         <a href="https://codeburst.io/deploy-react-to-github-pages-to-create-an-amazing-website-42d8b09cd4d">
         Deploy React to GitHub-Pages to create an amazing website! 
         </a>
         </p>
         <p>
         <a href="https://chvin.github.io/react-tetris/">
         Tetris! 
         </a>
         </p>
         
        </div>
      </div>
    );
  }
}

export default App;
