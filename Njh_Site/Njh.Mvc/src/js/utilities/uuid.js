const App__UUID = {
  
  generate: function (prefix = null, suffix = null) {

    let dt = new Date().getTime();
    let uuid = "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(
      /[xy]/g,
      function (c) {
        let r = (dt + Math.random() * 16) % 16 | 0;
        dt = Math.floor(dt / 16);
        return (c == "x" ? r : (r & 0x3) | 0x8).toString(16);
      }
    );

    if(prefix !== null) {
      uuid = `${prefix}-${uuid}`;
    }

    if(suffix !== null) {
      uuid = `${uuid}-${suffix}`;
    }
		
    return uuid;
  },
}

export default App__UUID;