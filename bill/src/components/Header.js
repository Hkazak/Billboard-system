import logo from "../pages/logo.png";
import "../styles/Header.css"

function Header({title})
{
    return (
      <div className="header-title">
          <img src={logo} alt="" className="logo"/>
          <h1 className="page-title">{title}</h1>
      </div>
    );
}

export default Header;