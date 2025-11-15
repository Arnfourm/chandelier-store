import "./App.css";
import { LogIn } from "./pages/LogIn";
import { Registration } from "./pages/Registration";
import { Home } from "./pages/Home";
import { Contacts } from "./pages/Contacts";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import { Catalog } from "./pages/Catalog";

function App() {
    return (
        <BrowserRouter>
            <Switch>
                <Route exact path="/" component={Home} />
                <Route path="/catalog" component={Catalog} />
                <Route path="/contacts" component={Contacts} />
                <Route path="/login" component={LogIn} />
                <Route path="/reg" component={Registration} />
            </Switch>
        </BrowserRouter>
    );
}

export default App;
