import React from 'react'
import {Input} from "@progress/kendo-react-inputs";


const Kpi = (props) => (

         <div className="row" >
             <div className="col-sm-1">
                 <input type="checkbox" />
             </div>
             <div className="col-sm-7">
                 <Input readOnly value={props.name} style={{width:'100%'}}/>   
             </div>
             <div className="col-sm-1">
                 <Input value={props.target} style={{width:'100%'}}/>
             </div>
             <div className="col-sm-1">
                 <Input value={props.target} style={{width:'100%'}}/>
             </div>
             <div className="col-sm-1">
                 <Input value={props.target} style={{width:'100%'}}/>
             </div>
             <div className="col-sm-1">
                 <Input value={props.target} style={{width:'100%'}}/>
             </div>
         </div>  
   
)

export default  Kpi