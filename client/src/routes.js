import React from "react";
import { 
    Navigate, 
    Routes, 
    Route, 
    BrowserRouter as Router } from "react-router-dom";
import RouteGuard from "./components/RouteGuard"

//history
import { history } from './helpers/history';

//pages
import HomePage from "./pages/HomePage"
import LoginPage from "./pages/Login"

function Nav() {
    return (
        <Router history={history}>
            <Routes>
                <RouteGuard
                    exact
                    path="/"
                    component={HomePage}
                />
                <Route
                    path="/login"
                    component={LoginPage}
                />
                <Navigate to="/" />
            </Routes>
        </Router>
    );
}

export default Nav