import { observer } from 'mobx-react-lite';
import React from 'react'
import { Link } from 'react-router-dom';
import { Button, Container } from 'semantic-ui-react'
import { useStore } from '../../app/stores/store';

export default observer(function GameChoose() {
    const {gameStore, userStore} = useStore();
    return (
        <Container>
            <Button onClick={() => gameStore.create(userStore.user!)} positive>Create game</Button>
            <Button as={Link} to='/gamelist' positive>Connect to game</Button>
        </Container>
    )
})
