import type { GreetMessageObject } from "../types/contractTypes";
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
        case FunctionTypes.sayHelloFromObject: {
            return async () => {
                const messageObject: GreetMessageObject = {
                    Name: "Javascript",
                    Message: "This is a message inside a JS object!"
                };

                // Note this must be run with the "sync" modifier as it is returned as a proxy object. 
                // Otherwise accessing each property must also be awaited.
                // Only single argument function calls are supported by .Net so the object must be serialized and de-serialized. 
                const response: GreetMessageObject = await window?.chrome.webview.hostObjects.sync.GreetMessageContract.SayHelloWithObject(
                    JSON.stringify(messageObject)
                );

                const responseMessage = `Hello from ${response.Name}, message is ${response.Message}`

                alert(responseMessage);
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

