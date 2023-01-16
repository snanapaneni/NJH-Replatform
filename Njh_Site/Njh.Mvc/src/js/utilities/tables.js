const App__tables = {
  makeResponsive: function () {
    const tables = Array.from(document.querySelectorAll(".app-main table"));

    tables.map((table) => {
      // Create the wrapper
      const wrapper = document.createElement("div");
      wrapper.classList.add("table-responsive");

      // Move the div inside the parent above the table.
      table.parentNode.insertBefore(wrapper, table);

      // Move the table inside the wrapper.
      wrapper.appendChild(table);

      // get lunch.
    });
  },
};

export default App__tables;
