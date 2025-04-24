function downloadFileFromBase64(fileName, base64String, mimeType) {
    const link = document.createElement("a");
    link.href = `data:${mimeType};base64,${base64String}`;
    link.download = fileName;
    link.click();
}