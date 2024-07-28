function createShopContainerStyles(containerClass, top, left) {
    if (!document.querySelector(`.${containerClass}`)) {
        const style = document.createElement('style');
        style.textContent = `
                                .${containerClass} {
                                    position: absolute;
                                    top: ${top}px;
                                    left: ${left}px;
                                    box-shadow: 0 4px 4px rgba(0, 0, 0, 0.5);
                                    width: 355px;
                                    height: 411px;
                                }
                            `;
        document.head.appendChild(style);
    }
}
const itemsPerRow = 3;
const itemsPerPage = 6;
const currentPage = 2;
const topOffset = 100;
const rowHeight = 450;
const rowMarginOffsetFirstRow = -2680;
const rowMarginOffsetSecondRow = -2600;
const rowMarginOffsetThirdRow = -3580;
const rowMarginOffsetFourthRow = -3520;
const rowMarginOffsetFifthRow = -4500;
const rowMarginOffsetSixthRow = -4400;
const rowMarginOffsetSeventhRow = -5390;
const rowMarginOffsetEightRow = -5290;
const rowMarginOffsetNineRow = -6300;
const rowMarginOffsetTenRow = -6200;


for (let i = 1; i <= 30; i++) {
    const containerClass = `shop-container-${i}`;
    const row = Math.floor((i - 1) / itemsPerRow);
    const col = (i - 1) % itemsPerRow;

    let top = row * 450;

    if (row === 0) {
        top += rowMarginOffsetFirstRow;
    }

    if (row === 1) {
        top += rowMarginOffsetSecondRow;
    }

    if (row === 2) {
        top += rowMarginOffsetThirdRow;
    }

    if (row === 3) {
        top += rowMarginOffsetFourthRow;
    }
    if (row === 4) {
        top += rowMarginOffsetFifthRow;
    }
    if (row === 5) {
        top += rowMarginOffsetSixthRow;
    }
    if (row === 6) {
        top += rowMarginOffsetSeventhRow;
    }
    if (row === 7) {
        top += rowMarginOffsetEightRow;
    }
    if (row === 8) {
        top += rowMarginOffsetNineRow;
    }
    if (row === 9) {
        top += rowMarginOffsetTenRow;
    }
    top += (currentPage - 1) * itemsPerPage * rowHeight;

    const left = col * 487;
    createShopContainerStyles(containerClass, top, left);
}