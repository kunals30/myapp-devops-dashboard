function loadProductsAsCards() {
    fetch('/api/product')
        .then(res => res.json())
        .then(data => {
            const container = document.getElementById('productCards');
            if (!container) return;

            container.innerHTML = '';

            data.forEach(p => {
                container.innerHTML += `
                    <div class="card">
                        <h3>${p.name}</h3>
                        <p>Price: Rs. ${p.price}</p>
                        <p>ID: ${p.id}</p>
                    </div>
                `;
            });
        });
}

function addProduct() {
    const name = document.getElementById('name').value;
    const price = document.getElementById('price').value;
    const message = document.getElementById('message');

    if (!name || !price) {
        message.innerText = "Please fill all fields.";
        return;
    }

    fetch('/api/product', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name, price: parseFloat(price) })
    })
    .then(res => res.json())
    .then(() => {
        message.innerText = "Product added successfully!";
        document.getElementById('name').value = '';
        document.getElementById('price').value = '';
    })
    .catch(() => {
        message.innerText = "Error adding product.";
    });
}
