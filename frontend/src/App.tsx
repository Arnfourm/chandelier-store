import "./App.css";
import { BrowserRouter, Route, Switch } from "react-router-dom";

import { Home } from "./pages/Home";
import { Catalog } from "./pages/Catalog";
import { Contacts } from "./pages/Contacts";
import { LogIn } from "./pages/LogIn";
import { Registration } from "./pages/Registration";
import { UserAccount } from "./pages/UserAccount";
import { AdminPanel } from "./pages/AdminPanel";

function App() {
    return (
        <BrowserRouter>
            <Switch>
                <Route exact path="/" component={Home} />
                <Route path="/catalog" component={Catalog} />
                <Route path="/contacts" component={Contacts} />
                <Route path="/login" component={LogIn} />
                <Route path="/reg" component={Registration} />
                <Route path="/account" component={UserAccount} />
                <Route path="/employee" component={AdminPanel} />
            </Switch>
        </BrowserRouter>
    );
}

export default App;
