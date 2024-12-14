if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', setupScrollPlugin);
} else {
    setupScrollPlugin();
}

function setupScrollPlugin() {
    const main = document.querySelector('main:first-of-type article:first-of-type');
    
    const grids = main.querySelectorAll("div.e-content");
    
    grids.forEach(grid => {
        setupGridScrollBar(grid, main);
    });
}

/**
 * @param {HTMLDivElement} grid
 * @param {HTMLElement} main
 */
function setupGridScrollBar(grid, main) {
    const scrollbarId = `custom-bottom-scroll-bar`;
    
    // Check if bar already exists - then remove - finally add the new bar
    const existingBar = document.getElementById(scrollbarId);
    
    if (existingBar){
        existingBar.remove()
    }

    createFixedScrollBar(scrollbarId, main);
    
    const fixedScrollBar = document.getElementById(scrollbarId);

    grid.addEventListener('scroll', (e) => {
        e.preventDefault();
    });
    
    // setting up the "scroll-pill"
    setupThumbDrag(grid, fixedScrollBar);

    adjustThumbSize(grid, fixedScrollBar);
    syncThumbPosition(grid, fixedScrollBar);
    
    // Handle page resizes 
    main.addEventListener("resize", () => {
        adjustThumbSize(grid, fixedScrollBar);
        syncThumbPosition(grid, fixedScrollBar);
    });

    // using an observer to handle the removal of the scrollbar when no longer needed
    const observer = new MutationObserver((mutationsList) => {
        for (const mutation of mutationsList) {
            if (mutation.type === 'childList') {
                fixedScrollBar.remove();
            }
        }
    });

    const config = { childList: true };
    
    observer.observe(main, config);
}

/**
 * @param {string} id
 * @param {HTMLElement} main
 */
function createFixedScrollBar(id, main) {
    const scrollBar = document.createElement("div");
    scrollBar.id = id;
    scrollBar.classList.add("fixed-scrollbar");

    const scrollBarThumb = document.createElement("div");
    scrollBarThumb.classList.add("scroll-position");
    scrollBarThumb.style.transform = "translateX(0)";
    scrollBar.appendChild(scrollBarThumb);

    main.appendChild(scrollBar);
}

/**
 * @param {HTMLDivElement} grid
 * @param {HTMLElement} fixedScrollBar
 */
function setupThumbDrag(grid, fixedScrollBar) {
    const scrollThumb = fixedScrollBar.firstElementChild;
    
    scrollThumb.addEventListener("mousedown", (e) => {
        const thumbStartX = e.clientX;
        const thumbLeftStart = parseFloat(scrollThumb.style.transform.replace("translateX(", "").replace("px)", "")) || 0;

        const onMouseMove = (e) => {
            const deltaX = e.clientX - thumbStartX;
            const maxThumbLeft = fixedScrollBar.clientWidth - scrollThumb.clientWidth;
            const newThumbLeft = Math.max(0, Math.min(maxThumbLeft, thumbLeftStart + deltaX));
            const scrollPercentage = newThumbLeft / maxThumbLeft;

            scrollThumb.style.transform = `translateX(${newThumbLeft}px)`;

            grid.scrollLeft = scrollPercentage * (grid.scrollWidth - grid.clientWidth);
        };

        const onMouseUp = () => {
            document.removeEventListener("mousemove", onMouseMove);
            document.removeEventListener("mouseup", onMouseUp);
        };

        document.addEventListener("mousemove", onMouseMove);
        document.addEventListener("mouseup", onMouseUp);
    });
}

function adjustThumbSize(grid, fixedScrollBar) {
    const scrollThumb = fixedScrollBar.firstElementChild;
    const visibleWidth = grid.clientWidth;
    const scrollableWidth = grid.scrollWidth;
    const thumbWidth = Math.max(50, (visibleWidth / scrollableWidth) * fixedScrollBar.clientWidth);

    scrollThumb.style.width = `${thumbWidth}px`;
}

function syncThumbPosition(grid, fixedScrollBar) {
    const scrollPercentage = grid.scrollLeft / (grid.scrollWidth - grid.clientWidth);
    const scrollBarThumb = fixedScrollBar.firstElementChild;
    const maxThumbLeft = fixedScrollBar.clientWidth - scrollBarThumb.clientWidth;
    scrollBarThumb.style.transform = `translateX(${scrollPercentage * maxThumbLeft}px)`;
}
