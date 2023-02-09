const App__timers = {
  debounce: function (callback, time = 100) {
    var time = time || 100; // 100 by default if no param
    var timer;
    return function (event) {
      if (timer) clearTimeout(timer);
      timer = setTimeout(callback, time, event);
    };
  },
};

export default App__timers;
