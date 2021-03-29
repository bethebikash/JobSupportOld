import React from 'react';
import { useHistory } from 'react-router-dom';
import { Menu } from 'antd';

function CustomMenu() {
  const history = useHistory();

  const handleVisit = (path: string) => {
    history.push(path);
  };

  return (
    <Menu theme="dark" mode="horizontal">
      <Menu.Item onClick={() => handleVisit('/')}>Home</Menu.Item>
      <Menu.Item onClick={() => handleVisit('/user')}>Users</Menu.Item>
    </Menu>
  );
}

export default CustomMenu;
