import React from "react";
//import {  useEffect, useState } from "react";

interface VisibilityProps {
    loading?: boolean;
  }

  export function withLoading<P>(WrappedComponent: React.ComponentType<P>) {
    const VisibityControlled = (props: P & VisibilityProps) => {
      if (props.loading) {
        return <div>I am Loading</div>;
      }
  
      return <WrappedComponent {...props} />;
    };
  
    return VisibityControlled;
  }
  
/*
  interface WithLoadingProps {
    loading: boolean;
  }
  */
  
// function withLoading(WrappedComponent) {
//   return function LoadingComponent({ isLoading, ...props }) {
//     const [loading, setLoading] = useState(isLoading);
    
//     useEffect(() => {
//       setLoading(isLoading);
//     }, [isLoading]);

//     if (loading) {
//       return <div>Component is Loading...</div>;
//     }

//     return <WrappedComponent {...props} />;
//   };
// }