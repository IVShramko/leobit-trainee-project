import { Injectable } from "@angular/core";
import { DomSanitizer, SafeResourceUrl } from "@angular/platform-browser";

@Injectable({
    providedIn: 'root'
})
export class ImageUtility {

    constructor(private sanitizer: DomSanitizer) {
    }

    ConvertToBase64(file: File) {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.readAsDataURL(file);

            reader.onload = () => resolve(reader.result);
            reader.onerror = (error) => reject(error);
        });
    }

    ConvertToSafeResourceUrl(base64: string, name: string): SafeResourceUrl {
        const extension = name.split('.').pop();
        return this.sanitizer.bypassSecurityTrustResourceUrl(
            `data:image/${extension};base64,` + base64 as string);
    }

    ConvertToStringUrl(base64: string, name: string): string {
        const extension = name.split('.').pop();
        return `data:image/${extension};base64,` + base64;
      }
}