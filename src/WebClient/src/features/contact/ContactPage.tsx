import {
  Box,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Typography,
} from "@mui/material";
import { Business, Email, LocalPhone } from "@mui/icons-material";

export default function ContactPage() {
  return (
    <>
      <Typography variant="h2">Contact Us</Typography>
      <Box sx={{ m: 2 }}>
        <List>
          <ListItem>
            <ListItemIcon>
              <Email />
            </ListItemIcon>
            <ListItemText
              primary="Email: nguyendinhanhvlqt@gmail.com"
              primaryTypographyProps={{ variant: "h6" }}
            />
          </ListItem>
          <ListItem>
            <ListItemIcon>
              <LocalPhone />
            </ListItemIcon>
            <ListItemText
              primary="1111 1111"
              primaryTypographyProps={{ variant: "h6" }}
            />
          </ListItem>
          <ListItem>
            <ListItemIcon>
              <Business />
            </ListItemIcon>
            <ListItemText
              primary="Thu Duc, Ho Chi Minh City, Vietnam"
              primaryTypographyProps={{ variant: "h6" }}
            />
          </ListItem>
        </List>
      </Box>
    </>
  );
}
