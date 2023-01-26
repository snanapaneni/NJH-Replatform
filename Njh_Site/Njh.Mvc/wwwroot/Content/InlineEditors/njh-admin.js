/**
 *
 * Methods used to enhance the admin experience.
 *
 */
window.NJHAdmin = {
  correctAnchorSpanButtons: (target = "a > span.btn") => {
    // Get only links that have span.btn as direct children.
    const buttons = document.querySelectorAll(target);

    if (!buttons || buttons.length === 0) return;

    // ! TODO: Remove all console.logs when ready for prod.
    console.log(`Correcting ${buttons.length} buttons`);

    // Loop over the spans.
    buttons.forEach(function (span) {
      const anchor = span.parentElement;

      // Bail if there are no classes
      if (!span.classList.length) return;

      // Add all classes from the span to the anchor.
      span.classList.forEach((spanClass) => {
        anchor.classList.add(spanClass);
      });

      // Replace the span with just the text of the button.
      anchor.innerHTML = span.textContent;
    });
  },

  correctSpanAnchorButtons: (target = "span.btn > a") => {
    // Get only links that have span.btn as direct parents of anchors.
    const buttons = document.querySelectorAll(target);

    if (!buttons || buttons.length === 0) return;

    // ! TODO: Remove all console.logs when ready for prod.
    console.log(`Correcting ${buttons.length} buttons`);

    // Loop over the anchors.
    buttons.forEach(function (anchor) {
      const span = anchor.parentElement;

      // Bail if there are no classes
      if (!span.classList.length) return;

      // Add all classes from the span to the anchor.
      span.classList.forEach((spanClass) => {
        anchor.classList.add(spanClass);
      });

      // Insert the anchor before the span
      span.parentNode.insertBefore(anchor, span);

      // Delete the span from the DOM
      span.remove();
    });
  },
};
