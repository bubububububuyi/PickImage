//
//  IAPManager.h
//  Unity-iPhone
//
//  Created by MacMini on 15-11-20.
//
//


#import <Foundation/Foundation.h>
#import <StoreKit/StoreKit.h>


@interface IAPManager : NSObject<SKProductsRequestDelegate, SKPaymentTransactionObserver>{
    SKProduct *proUpgradeProduct;
    SKProductsRequest *productsRequest;

}


-(void)attachObserver;
-(BOOL)CanMakePayment;
-(void)requestProductData:(NSString *)productIdentifiers;
-(void)buyRequest:(NSString *)productIdentifier;


@end