import { Box, Typography, Container } from '@mui/material';

function Footer() {
  return (
    <Box component="footer" sx={{ py: 3, textAlign: 'center', backgroundColor: '#f1f1f1', width: '100%', mt: 'auto' }}>
      <Container>
        <Typography variant="body2" color="textSecondary">
          © {new Date().getFullYear()} NDA's Shop. Test for ASP.NET Core microservices backend.
        </Typography>
      </Container>
    </Box>
  );
}

export default Footer;
