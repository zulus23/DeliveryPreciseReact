import React from 'react';

const FlashMessage = (props) => {
    console.log(props);
    return (
        <div className="flash-error">
            {props.message}
        </div>
    );
};

FlashMessage.defaultProps = {
    message:"Произошла ошибка"
}
export default FlashMessage;