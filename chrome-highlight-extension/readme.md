# Features

1. Use commas (',') to seperate Managers.


Reference:
- http://james.padolsey.com/javascript/replacing-text-in-the-dom-solved/
- http://stackoverflow.com/questions/2582831/how-can-i-highlight-the-text-of-the-dom-range-object
- https://chrome.google.com/webstore/detail/multi-highlight/pfgfgjlejbbpfmcfjhdmikihihddeeji

Forked from:
https://github.com/WindzCUHK/chrome-highlight-extension

Bug:
- There may be change in DOM structure if the keyword spans across multi HTML tag.
- When page init, no content in body. Currently, hard-coded a delay, may investigate using MutationObserver() to solve it.
