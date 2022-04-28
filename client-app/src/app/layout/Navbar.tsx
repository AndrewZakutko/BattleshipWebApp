import { observer } from 'mobx-react-lite'
import React from 'react'
import { Link } from 'react-router-dom'
import { Container, Dropdown, Menu } from 'semantic-ui-react'
import { useStore } from '../stores/store'

export default observer(function Navbar() {
  const { userStore: {user, logout, isLoggedIn} } = useStore();
  return (
      <Menu>
        <Container>
          <Menu.Item as={Link} to='/'>
            BattleShip Game
          </Menu.Item>
          <Menu.Item as={Link} to='/gamechoose'>
            Choose Game
          </Menu.Item>
          {isLoggedIn ? (
            <Menu.Item position='right'>
              <Dropdown pointing='top left' text={user!.name}>
                <Dropdown.Menu>
                  <Dropdown.Item text='History'></Dropdown.Item>
                  <Dropdown.Item onClick={logout} text='Logout'></Dropdown.Item>
                </Dropdown.Menu>
              </Dropdown>
            </Menu.Item>)
          :
            (
                <Menu.Item as={Link} to='/login' position='right'>Login</Menu.Item>
            )
          }
        </Container>
      </Menu>
  )
})
