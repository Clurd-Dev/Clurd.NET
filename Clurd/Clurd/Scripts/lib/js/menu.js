export function rightClick(e) {
	if(e.target.id != ""){
		e.preventDefault();
		document.getElementById('contextMenu').style.display = 'block';
		var menu = document.getElementById('contextMenu');
		menu.style.display = 'block';
		menu.style.left = e.pageX + 'px';
		menu.style.top = e.pageY + 'px';
		return e.target.id;
	}else{
		return "0";
	}

}
export function hideMenu() {
	document.getElementById('contextMenu').style.display = 'none';
}
