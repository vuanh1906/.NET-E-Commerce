import { AppBar, Toolbar, Typography } from "@mui/material";
import CustomizedSwitch from "../../features/CustomizeSwitch";

interface Props {
    darkMode: boolean;
    handleThemeChange: () => void;
}
export default function Header({ darkMode, handleThemeChange }: Props) {
    return (

        <AppBar position="static" sx={{ mb: 4 }}>
            <Toolbar>
                <Typography variant="h6" >
                    Skinet
                </Typography>
                <CustomizedSwitch handleThemeChange={handleThemeChange} darkMode={darkMode} />
            </Toolbar>
        </AppBar>
    )
} 