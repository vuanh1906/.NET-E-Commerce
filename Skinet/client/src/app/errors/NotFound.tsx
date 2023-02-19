import { Button, Container, Divider, Paper, Typography } from "@mui/material";
import { Link } from "react-router-dom";

export default function NotFound() {
    return (
        <Container component={Paper} >
            <Typography gutterBottom variant="h4">Oops - we could not find what u r looking 4</Typography>
            <Divider/>
            <Button fullWidth component={Link} to='/catalog'>Go back to shop</Button>
        </Container>
    )
}