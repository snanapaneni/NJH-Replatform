const App__alphasort = {
  element: null,

  init: function () {
    this.element = document.querySelector(".alphasort");

    if (this.element === null) return;

    console.log("alphasort", this.element);
  },
};

export default App__alphasort;
