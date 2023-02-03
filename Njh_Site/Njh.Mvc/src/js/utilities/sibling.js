const App__sibling = {
  getNext: function (elem, callback) {
    let sibling = elem.nextElementSibling;
    if (!callback || typeof callback !== "function") return sibling;
    let index = 0;
    while (sibling) {
      if (callback(sibling, index, elem)) return sibling;
      index++;
      sibling = sibling.nextElementSibling;
    }
  },

  getAll: function (elem) {
    // Get's all siblings of a specific element.
    let siblings = [];

    if (!elem.parentNode) return;

    let sibling = elem.parentNode.firstChild;

    while (sibling) {
      while (sibling) {
        if (sibling.nodeType === 1 && sibling !== elem) {
          siblings.push(sibling);
        }
        sibling = sibling.nextSibling;
      }
      return siblings;
    }
  },

  getLast: function (elem) {
    if (!elem.parentNode) return;

    return elem.parentNode.lastChild;
  },
  
  getFirst: function (elem) {
    if (!elem.parentNode) return;

    return elem.parentNode.firstChild;
  },
};

export default App__sibling;

