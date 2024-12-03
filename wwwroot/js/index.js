window.initializeGrid = (dotNetHelper, gridElement) => {
    const grid = gridElement;
    if (!grid) {
        console.error('Grid element not found.');
        return;
    }

    // Function to apply colors based on the inventory data
    const applyColors = () => {
        const rows = grid.querySelectorAll(".e-row");
        rows.forEach(row => {
            const inventoryData = row.getAttribute('data-inventory');
            if (!inventoryData) {
                console.warn('No inventory data found for row:', row);
                return;
            }

            const inventory = JSON.parse(inventoryData);
            console.log('Applying color for inventory:', inventory);

            if (inventory.SelectedScope === "Row") {
                row.style.backgroundColor = inventory.Selectedcolor.toLowerCase();
                console.log(`Row color set to ${inventory.Selectedcolor.toLowerCase()}`);
            } else if (inventory.SelectedScope === "Cell") {
                const cells = row.querySelectorAll(".e-rowcell");
                cells.forEach(cell => {
                    if (cell.getAttribute('data-field') === 'TagFirstPart') {
                        cell.style.backgroundColor = inventory.Selectedcolor.toLowerCase();
                        console.log(`Cell color set to ${inventory.Selectedcolor.toLowerCase()} for field TagFirstPart`);
                    }
                });
            }
        });
    };

    // Apply colors initially
    applyColors();

    // Re-apply colors whenever the grid data changes
    grid.addEventListener('dataBound', applyColors);
};