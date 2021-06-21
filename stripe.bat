@echo off
echo Running Stripe localhost
cd api
stripe listen -f https://localhost:5001/api/payment/webhook
pause