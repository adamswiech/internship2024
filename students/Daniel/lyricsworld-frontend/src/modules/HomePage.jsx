import '../App.css';
import Header from "./Header"
import React from 'react'

function HomePage() {  
  return (
    <div className="App">
      <Header />
      <main>
        <h1>LyricsWorld</h1>
        <h2>Music, the Coolest Language of All</h2>
        <a>Start Searching</a>
      </main> 
    </div>
        );
}

export default HomePage
