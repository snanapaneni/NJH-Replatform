const observerOptions = {
	root: null,
	threshold: 0.15,
	rootMargin: '0px 0px -50px 0px'
};

const observer = new IntersectionObserver(entries => {
	
	entries.forEach(entry => {
		if (entry.isIntersecting) {
			entry.target.classList.add('animation');
			observer.unobserve(entry.target);
		}
	});
}, observerOptions);

window.addEventListener('DOMContentLoaded', (event) => {
	
	const sections = Array.from(document.getElementsByClassName('animate'));
	
	for (let section of sections) {
		observer.observe(section);
	}
});
