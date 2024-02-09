import { Routes, Route } from 'react-router-dom'
import { Container } from 'react-bootstrap'
import { Home } from './pages/Home'
import { Items } from './pages/Items'
import { Packlists } from './pages/Packlists'
import { Users } from './pages/Users'
import { Navbar } from './Components/Navbar'
function App() {

  return (
    <>
    <Navbar />
     <Container className="mb-4">
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/users" element={<Users />} />
        <Route path="/items" element={<Items />} />
        <Route path="/packlists" element={<Packlists />} />

      </Routes>
     </Container>
    </>
  )
}

export default App
