// src/components/Header.js
import React from 'react';
import { Link } from 'react-router-dom';
import { Menu } from 'antd';

const Header = () => {
    return (
        <Menu mode="horizontal">
            <Menu.Item key="distributors">
                <Link to="/distributors">Distributors</Link>
            </Menu.Item>
            <Menu.Item key="products">
                <Link to="/products">Products</Link>
            </Menu.Item>
            <Menu.Item key="sales">
                <Link to="/sales">Sales</Link>
            </Menu.Item>
            <Menu.Item key="reports">
                <Link to="/reports">Report</Link>
            </Menu.Item>
        </Menu>
    );
};

export default Header;
