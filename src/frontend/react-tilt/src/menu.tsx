import { Link } from "react-router-dom";

function MenuLeft(): JSX.Element {
    return <>
        <div>
            <Link to="/">Home</Link>
        </div><div>
            <Link to="/public/ignatandrei">ignatandrei</Link>
        </div>

    </>
}


export default MenuLeft;