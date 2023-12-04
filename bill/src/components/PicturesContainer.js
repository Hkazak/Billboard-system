import "../styles/PictuersContainer.css";

function PicturesContainer({pictures}) {
    return (
        <div className="pictures-container">
            {pictures?.map(e => <img className="uploaded-image" src={e.data} key={e.id} alt={e.name} width="100"
                                     height="100"/>)}
        </div>
    );
}

export default PicturesContainer;