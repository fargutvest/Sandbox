package com.example.reactiverestservice;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class ReactiveRestServiceApplication {

	public static void main(String[] args) {
		SpringApplication.run(ReactiveRestServiceApplication.class, args);

		GreetingWebClient gwc = new GreetingWebClient();
		System.out.println(gwc.getResult());
	}

}
