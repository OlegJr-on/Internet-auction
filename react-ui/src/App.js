import { Header } from "./layout/Header";
import { Footer } from "./layout/Footer";
import { Main } from "./layout/Main";
import {BrowserRouter} from 'react-router-dom'


function App() {

  return (
    <>
    <BrowserRouter>
        <Header />
        <Main />
        <Footer />
    </BrowserRouter>
    </>
    );
}

export default App;
