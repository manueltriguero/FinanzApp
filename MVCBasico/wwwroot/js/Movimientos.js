const celdaImporte = document.querySelectorAll('tbody td:nth-child(3)');

celdaImporte.forEach((celda) => {
    const importe = celda.textContent.trim();
    if (importe.startsWith('-', 1)) {
        celda.style.color = 'red';
    } else{
        celda.style.color = 'green';
    }
});