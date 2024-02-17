import { useRef } from "react";
/**
 * @param {{
 *  children: import("react").ReactElement,
 *  isModalOpen: Boolean,
 *  closeModal: () => void
 * }} props
 */
function Modal(props) {
  const { isModalOpen, closeModal } = props;
  const backdropRef = useRef();

  /** @param {MouseEvent} e */
  const useCloseModal = (e) => {
    if (e.target !== backdropRef.current) return;
    closeModal();
  };
  return (
    <dialog className="modal" open={isModalOpen}>
      <div ref={backdropRef} className="backdrop" onClick={useCloseModal}>
        {props.children}
      </div>
    </dialog>
  );
}

export default Modal;
