function downloadFileFromBase64(fileName, base64String, mimeType) {
    const link = document.createElement("a");
    link.href = `data:${mimeType};base64,${base64String}`;
    link.download = fileName;
    link.click();
}

function redirectToCheckout(sessionId) {
    const stripe = Stripe('pk_test_51RHTepH8Eh2h5xHttcVBC86GELtFrlbst8NKGwoXq9d2m2Atm4VQsAEITLjK5ZF6fsCFdNp1JtOiylWeyjY0nsml00Y8uCQMOz');
    stripe.redirectToCheckout({ sessionId: sessionId });
}
