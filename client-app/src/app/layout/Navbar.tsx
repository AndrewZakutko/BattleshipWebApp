import { observer } from 'mobx-react-lite';
import React from 'react';
import { Link } from 'react-router-dom';
import { Container, Dropdown, Menu } from 'semantic-ui-react';
import { useStore } from '../stores/store';

export default observer(function Navbar() {
  const { userStore } = useStore();
  return (
      <Menu size='large' inverted>
        <Container>
          <Menu.Item>
            Battleship Game
          </Menu.Item>
          {userStore.isLoggedIn ? (
            <Menu.Menu position='right'>
              {userStore.game!.id == "00000000-0000-0000-0000-000000000000" || userStore.game!.id == null ? 
              (
                <>
                  <Menu.Item>
                    <Dropdown text='Actions'>
                      <Dropdown.Menu>
                        <Dropdown.Item onClick={() => userStore.createGame(userStore.user!)} text='Create a game'/>
                      </Dropdown.Menu>
                    </Dropdown>
                  </Menu.Item>
                  <Menu.Item>
                    <Dropdown text={`Hi, ${userStore.user!.name}!`}>
                      <Dropdown.Menu>
                        <Dropdown.Item onClick={userStore.logout} text='Logout'/>
                        <Dropdown.Item as={Link} to={'/gamehistory'} text='History'/>
                        <Dropdown.Item as={Link} to={'/gamelist'} text='List of games'/>
                      </Dropdown.Menu>
                    </Dropdown>
                  </Menu.Item>
                </>
              )
              :
              (
                <>
                  <Menu.Item>
                    <Dropdown text={`Hi, ${userStore.user!.name}!`}>
                      <Dropdown.Menu>
                        <Dropdown.Item onClick={userStore.logout} text='Logout'/>
                      </Dropdown.Menu>
                    </Dropdown>
                  </Menu.Item>
                </>
              )}
            </Menu.Menu>)
          :
            (
              <>
                <Menu.Item as={Link} to='/' position='right'>Login</Menu.Item>
              </>
            )
          }
        </Container>
      </Menu>
  )
})
