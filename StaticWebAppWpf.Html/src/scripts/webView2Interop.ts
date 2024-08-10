import { FunctionTypes } from "../types/FunctionTypes";

export const GetFunctionByName = (functionType: string) => {
    switch (functionType) {
        case FunctionTypes.sayHello: {
            return async () => {

                // The webView2 message contracts may be called directly using the contract name followed by the function name.
                const response = await window?.chrome.webview.hostObjects.GreetMessageContract.SayHello(
                    "JavaScript"
                );

                alert(response);
            }
        }
        case FunctionTypes.sayGoodbye: {
            return async () => {
                const response = await window?.chrome.webview.hostObjects.GreetMessageContract.SayGoodbye(
                    "JavaScript"
                );

                alert(response);
            }
        }
        case FunctionTypes.sayHelloAfterWait: {
            return async () => {
                const response = await window?.chrome.webview.hostObjects.GreetMessageContract.SayHelloAfterWait(
                    "JavaScript"
                );

                alert(response);
            }
        }
        case FunctionTypes.throwException: {
            return async () => {
                await window?.chrome.webview.hostObjects.GreetMessageContract.ThrowException();
            }
        }
        default:
            throw new Error("Unexpected function type");
    }
}

