function Footer(){
    return  <footer className="page-footer grey darken-2">
    <div className="container">
      <div className="row">
        <div className="col l6 s12">
          <h5 className="white-text">Contacts</h5>
          <p className="grey-text text-lighten-4">Manager: (099)-366-2022<br/>Support: (066)-416-4422 </p>
        </div>
        <div className="col l4 offset-l2 s12">
          <h5 className="white-text">Links</h5>
          <ul>
            <li><a className="grey-text text-lighten-3" href="https://www.instagram.com/copart1982/">Instagram</a></li>
            <li><a className="grey-text text-lighten-3" href="https://www.facebook.com/Copart/">Facebook</a></li>
            <li><a className="grey-text text-lighten-3" href="https://www.youtube.com/channel/UCM1W9Yjbr7kYj2P-rGTr4mw">YouTube</a></li>
          </ul>
        </div>
      </div>
    </div>
    <div className="footer-copyright">
      <div className="container">
      © {new Date().getFullYear()} Copyright Text
      </div>
    </div>
  </footer>
}

export {Footer}