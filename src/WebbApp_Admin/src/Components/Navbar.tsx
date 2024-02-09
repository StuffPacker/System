import { Button } from 'antd'
import { Container, Nav, Navbar as NavBs } from 'react-bootstrap'
import { NavLink } from 'react-router-dom'
import { LoginOutlined } from '@ant-design/icons';
import "../Styles/navbar.css";

export function Navbar() {
    return (
        <NavBs sticky="top" className='bg-light shadow-sm mb-3'>
            <Container>
                <div className="NavLinkContainer mx-1">
                    <img src="https://avatars.githubusercontent.com/u/55746806" className="md-5"></img>
                </div>
                <Nav className='me-auto'>
                    <Nav.Link to="/" as={NavLink} className="NavLinkContainer">
                        <span className="NavLinkText">Home</span>
                        <div className="NavLinkBackground"></div>
                    </Nav.Link>
                    <Nav.Link to="/users" as={NavLink} className="NavLinkContainer">
                        <span className="NavLinkText">Users</span>
                        <div className="NavLinkBackground"></div>
                    </Nav.Link>
                    <Nav.Link to="/items" as={NavLink} className="NavLinkContainer">
                        <span className="NavLinkText">Items</span>
                        <div className="NavLinkBackground"></div>
                    </Nav.Link>
                    <Nav.Link to="/packlists" as={NavLink} className="NavLinkContainer">
                        <span className="NavLinkText">Packlists</span>
                        <div className="NavLinkBackground"></div>
                    </Nav.Link>
                </Nav>
                <Button style={{ width: "3rem", height: "3rem", position: "relative" }} className='rounded-circle'>
                    <LoginOutlined />
                </Button>
            </Container>
        </NavBs>
    )
}