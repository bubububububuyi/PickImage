//
//  IAPManager.m
//  Unity-iPhone
//
//  Created by MacMini on 14-5-16.
//
//
#import "IAPManager.h"

@implementation IAPManager


-(void) attachObserver{
    NSLog(@"AttachObserver");
    [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
    

}


-(BOOL) CanMakePayment{
    return [SKPaymentQueue canMakePayments];
}


-(void) requestProductData:(NSString *)productIdentifiers{
    NSLog(@"--------3");
    NSArray *idArray = [productIdentifiers componentsSeparatedByString:@"\t"];
    NSSet *idSet = [NSSet setWithArray:idArray];
    [self sendRequest:idSet];

}


-(void)sendRequest:(NSSet *)idSet{
    NSLog(@"--------14");
    if([SKPaymentQueue canMakePayments])
    {
        NSLog(@"可以购买");
        SKProductsRequest *request = [[SKProductsRequest alloc] initWithProductIdentifiers:idSet];
        request.delegate = self;
        [request start];
    }
    else
    {
        NSLog(@"无权购买");
    }
}


-(void)productsRequest:(SKProductsRequest *)request didReceiveResponse:(SKProductsResponse *)response{
    NSLog(@"--------13");

    NSArray *products = response.products;
    for (SKProduct *p in products) {
//        UnitySendMessage("IOSBackMsg(Clone)", "ShowProductList", [[self productInfo:p] UTF8String]);
        NSLog(@"productInfo:%@",[self productInfo:p]);

    }
    
    for(NSString *invalidProductId in response.invalidProductIdentifiers){
        NSLog(@"Invalid product id:%@",invalidProductId);
    }
    
    //[request autorelease];
}


-(void)buyRequest:(NSString *)productIdentifier{
    NSLog(@"--------6");
    NSLog(@"productIdentifier:%@",productIdentifier);
    
//    NSString *list = productIdentifier;
//    NSLog(@"productKey:%@",list);
//    [self requestProductData:list];
//    
//
    SKPayment *payment = [SKPayment paymentWithProductIdentifier:productIdentifier];
    [[SKPaymentQueue defaultQueue] addPayment:payment];
//    NSSet *productIdentifiers = [NSSet setWithObject:productIdentifier];
//    productsRequest = [[SKProductsRequest alloc] initWithProductIdentifiers:productIdentifiers];
//    productsRequest.delegate = self; // your wrapper for IAP or AppDelegate or anything
//    [productsRequest start];
}


-(NSString *)productInfo:(SKProduct *)product{
    NSLog(@"--------6.2");

    NSArray *info = [NSArray arrayWithObjects:product.localizedTitle,product.localizedDescription,product.price,product.productIdentifier, nil];
    NSLog(@"product.productIdentifier:%@",product.productIdentifier);

    return [info componentsJoinedByString:@"\t"];
}


-(NSString *)transactionInfo:(SKPaymentTransaction *)transaction{
    NSLog(@"--------7");

    
    return [self encode:(uint8_t *)transaction.transactionReceipt.bytes length:transaction.transactionReceipt.length];
    
    //return [[NSString alloc] initWithData:transaction.transactionReceipt encoding:NSASCIIStringEncoding];
}


-(NSString *)encode:(const uint8_t *)input length:(NSInteger) length{
    static char table[] = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    
    NSMutableData *data = [NSMutableData dataWithLength:((length+2)/3)*4];
    uint8_t *output = (uint8_t *)data.mutableBytes;
    NSLog(@"--------7");

    for(NSInteger i=0; i<length; i+=3){
        NSInteger value = 0;
        for (NSInteger j= i; j<(i+3); j++) {
            value<<=8;
            
            if(j<length){
                value |=(0xff & input[j]);
            }
        }
        
        NSInteger index = (i/3)*4;
        output[index + 0] = table[(value>>18) & 0x3f];
        output[index + 1] = table[(value>>12) & 0x3f];
        output[index + 2] = (i+1)<length ? table[(value>>6) & 0x3f] : '=';
        output[index + 3] = (i+2)<length ? table[(value>>0) & 0x3f] : '=';
    }
    
    return [[NSString alloc] initWithData:data encoding:NSASCIIStringEncoding];
}


-(void) provideContent:(SKPaymentTransaction *)transaction{
    NSLog(@"--------8");
//    NSString * nsstring = transaction.transactionIdentifier;
//    const char * tempChar =[nsstring UTF8String];
//    NSData * _Nullable receipts= transaction.transactionReceipt;
//    const char * tempChar02=[receipts bytes];
//    NSString * encodingStr = [[transaction transactionReceipt] base64EncodedString];//获得验证订单去base64加密
    NSString * encodingStr=[[NSString alloc]initWithData:transaction.transactionReceipt encoding:NSUTF8StringEncoding];
    const char * tempChar =[encodingStr UTF8String];
    UnitySendMessage("Game Framework", "PangziBuyOk", tempChar);
}


-(void)paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray *)transactions{
    NSLog(@"--------9");

    for (SKPaymentTransaction *transaction in transactions) {
        switch (transaction.transactionState) {
            case SKPaymentTransactionStatePurchased://购买成功
            [self completeTransaction:transaction];
            break;
            case SKPaymentTransactionStateFailed:
            [self failedTransaction:transaction];
            break;
            case SKPaymentTransactionStateRestored:
            [self restoreTransaction:transaction];
            break;
            default:
            break;
        }
    }
}


-(void) completeTransaction:(SKPaymentTransaction *)transaction{
    NSLog(@"--------10");

    NSLog(@"Comblete transaction : %@",transaction.transactionIdentifier);
    [self provideContent:transaction];
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}


-(void) failedTransaction:(SKPaymentTransaction *)transaction{
    NSLog(@"--------11");
    UnitySendMessage("IOSBackMsg(Clone)", "PayBackHandle", "error");

    NSLog(@"Failed transaction : %@",transaction.transactionIdentifier);
    
    if (transaction.error.code != SKErrorPaymentCancelled) {
        NSLog(@"!Cancelled");
    }
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}


-(void) restoreTransaction:(SKPaymentTransaction *)transaction{
    NSLog(@"--------12");
    UnitySendMessage("IOSBackMsg(Clone)", "PayBackHandle", "error");

    NSLog(@"Restore transaction : %@",transaction.transactionIdentifier);
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}

- (void)dealloc{
    [[SKPaymentQueue defaultQueue] removeTransactionObserver:self];
    NSLog(@"-----removeTransactionObserver---");

//    [super dealloc];
}


@end
