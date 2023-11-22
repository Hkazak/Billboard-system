import React from 'react';
import Modal from 'react-modal';
import './page_styles/AN.css'

const AN = ({ isOpen, closeModal, title, children }) => {
    const customStyles = {
        content: {
          border: 'none',       // Remove the border
          boxShadow: 'none',    // Remove the box shadow
        //   opacity: 0,
        }
      };
  return (

    
    <Modal
      isOpen={isOpen}
      onRequestClose={closeModal}
      contentLabel={title}
      style={customStyles}
      ariaHideApp={false}
    >
        <div className='m-cont'>
            <div className="modal-content">
                <h2>{title}</h2>
                {children}
                <button className='close-btnnn' type="button" onClick={closeModal}>
                Close
                </button>
            </div>
        </div>
    </Modal>
  );
};


export default AN
